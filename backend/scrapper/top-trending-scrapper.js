const puppeteer = require('puppeteer');

require('https').globalAgent.options.ca = require('ssl-root-cas').create();
const axios = require('axios').default;
const apiBaseUrl = "https://localhost:5001/api/Repositories";
const fs = require('fs');

process.env['NODE_TLS_REJECT_UNAUTHORIZED'] = '0';

const topTrendingUrl = 'https://github.com/trending/c%23?since=daily';

(async () => {
    const browser = await puppeteer.launch();
    const page = await browser.newPage();
    await page.goto(topTrendingUrl);

    const rawArticles = await page.evaluate( () => Array.from( document.querySelectorAll( 'article' ), element => {
        var anchors = element.querySelectorAll('a');

        // second one should be what we search for
        return anchors[1].getAttribute('href');
    }));

    const baseGithubUrl = 'https://github.com';
    let articles = rawArticles.map(a => baseGithubUrl + a);

    // Send data
    axios.post(`${apiBaseUrl}`, {
        repositories: articles,
    })
    .then(function (response) {
        console.log('Success!');
    })
    .catch(function (error) {
        console.log(error);
    });

    await browser.close();
})();