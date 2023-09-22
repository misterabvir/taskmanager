export async function Post(url: string, model: Object): Promise<void> {
    await fetch(url, {
        method: 'POST',
        headers: { 'Content-type': 'application/json' },
        body: JSON.stringify(model)
    });
}

export async function Put(url: string, model: Object): Promise<void> {
    await fetch(url, {
        method: 'PUT',
        headers: { 'Content-type': 'application/json' },
        body: JSON.stringify(model)
    });
}

export async function Get(url: string): Promise<any> {
    const result = await fetch(url);
    return result.json();
}

export async function Delete(url: string, model: Object): Promise<void> {
    await fetch(url, {
        method: 'DELETE',
        headers: { 'Content-type': 'application/json' },
        body: JSON.stringify(model)
    });
}