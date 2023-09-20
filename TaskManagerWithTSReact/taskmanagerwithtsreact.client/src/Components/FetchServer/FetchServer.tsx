export async function PostRequestReturnable(url: string, model: Object): Promise<any> {   
    const result = await fetch(url, {
        method: 'POST',
        headers: { 'Content-type': 'application/json' },
        body: JSON.stringify(model)
      });
    return result.json();
}

export async function PostRequest( url: string, model: Object): Promise<void> {   
    await fetch(url, {
        method: 'POST',
        headers: { 'Content-type': 'application/json' },
        body: JSON.stringify(model)
      });
}

export async function GetRequest(url: string): Promise<any> {   
    const result = await fetch(url);
    return  result.json();
}