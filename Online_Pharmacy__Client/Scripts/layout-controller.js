async function LoadAndSetPageInto(id, url) {
    let page = await fetch(url);
    let content = await page.text();
    document.getElementById(id).innerHTML = content;
}




