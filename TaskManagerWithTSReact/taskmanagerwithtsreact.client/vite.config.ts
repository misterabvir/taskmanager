import { fileURLToPath, URL } from 'node:url';

import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';
import fs from 'fs';
import path from 'path';

const baseFolder =
    process.env.APPDATA !== undefined && process.env.APPDATA !== ''
        ? `${process.env.APPDATA}/ASP.NET/https`
        : `${process.env.HOME}/.aspnet/https`;

const certificateArg = process.argv.map(arg => arg.match(/--name=(?<value>.+)/i)).filter(Boolean)[0];
const certificateName = certificateArg ? certificateArg.groups.value : "taskmanagerwithtsreact.client";

if (!certificateName) {
    console.error('Invalid certificate name. Run this script in the context of an npm/yarn script or pass --name=<<app>> explicitly.')
    process.exit(-1);
}

const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);


export default defineConfig({
    plugins: [plugin()],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    server: {
        proxy: {
            '^/projectsList': {
                target: 'https://localhost:7060/',
                secure: false
            },
            '^/createProject': {
                target: 'https://localhost:7060/',
                secure: false
            },
            '^/saveTaskName': {
                target: 'https://localhost:7060/',
                secure: false
            },
            '^/startTask': {
                target: 'https://localhost:7060/',
                secure: false
            },
            '^/cancelTask': {
                target: 'https://localhost:7060/',
                secure: false
            },            
            '^/saveProjectName': {
                target: 'https://localhost:7060/',
                secure: false
            },
        },
        port: 5173,
        https: {
            key: fs.readFileSync(keyFilePath),
            cert: fs.readFileSync(certFilePath),
        }
    }
})
