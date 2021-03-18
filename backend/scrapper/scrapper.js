const puppeteer = require('puppeteer');

require('https').globalAgent.options.ca = require('ssl-root-cas').create();
const axios = require('axios').default;
const apiBaseUrl = "https://localhost:5001/api/solution";
const repositoryBaseUrl = "https://localhost:5001/api/repositories";
const fs = require('fs');

process.env['NODE_TLS_REJECT_UNAUTHORIZED'] = '0';

// var sslRootCAs = require('ssl-root-cas')
// sslRootCAs.inject()

let pages = [
  "https://github.com/dotnet-architecture/eShopOnDapr",
  "https://github.com/microsoft/MixedRealityToolkit-Unity",
  "https://github.com/Azure/bicep",
  "https://github.com/dotnet/maui",
  "https://github.com/Cysharp/UniTask",
  "https://github.com/proxysu/ProxySU",
  "https://github.com/AvaloniaUI/Avalonia",
  "https://github.com/dotnet/orleans",
  "https://github.com/microsoft/PowerToys",
  "https://github.com/dotnet/efcore",
  "https://github.com/microsoft/PowerToys",
  "https://github.com/coverlet-coverage/coverlet",
  "https://github.com/IdentityServer/IdentityServer4"
];

(async () => {

    async function getRepositoriesToScrap()
    {
        return axios.get(`${repositoryBaseUrl}`);
    }

    async function getStars(page)
    {
      return await page.evaluate(() => {
        let element = document.querySelector('#js-repo-pjax-container > div.color-bg-secondary.pt-3.hide-full-screen.mb-5 > div.d-flex.mb-3.px-3.px-md-4.px-lg-5 > ul > li:nth-child(2) > a.social-count.js-social-count');

        if (element == null)
        {
          element = document.querySelector('#js-repo-pjax-container > div.color-bg-secondary.pt-3.hide-full-screen.mb-5 > div.d-flex.mb-3.px-3.px-md-4.px-lg-5 > ul > li:nth-child(3) > a.social-count.js-social-count');
        }

        var starsCount = element.textContent
          .replaceAll(/\n/g, '')
          .replaceAll(' ', '');
        return starsCount;
      });
    }

    async function getAbout(page)
    {
      return await page.evaluate(() => {
        let element = document.querySelector('#repo-content-pjax-container > div > div.gutter-condensed.gutter-lg.flex-column.flex-md-row.d-flex > div.flex-shrink-0.col-12.col-md-3 > div > div.BorderGrid-row.hide-sm.hide-md > div > p');
        return element.textContent
          .replaceAll(/\n/g, '')
          .replaceAll('  ', '');
      });
    }

    async function getLicense(page)
    {
      return page.evaluate(() => {

        let element = document.querySelector('#repo-content-pjax-container > div > div.gutter-condensed.gutter-lg.flex-column.flex-md-row.d-flex > div.flex-shrink-0.col-12.col-md-3 > div > div.BorderGrid-row.hide-sm.hide-md > div > div:nth-child(9) > a');
        return 'hello';// element.textContent;
      });
    }

    async function getLastCommitDate(page)
    {
      return await page.evaluate(() => {
        let element = document.querySelector('#repo-content-pjax-container > div > div.gutter-condensed.gutter-lg.flex-column.flex-md-row.d-flex > div.flex-shrink-0.col-12.col-md-9.mb-4.mb-md-0 > div.Box.mb-3 > div.Box-header.Box-header--blue.position-relative > div > div.flex-1.d-flex.flex-items-center.ml-3.min-width-0 > div.d-flex.flex-auto.flex-justify-end.ml-3.flex-items-baseline > a:nth-child(2) > relative-time');
        if (element == null)
        {
          element = document.querySelector('#repo-content-pjax-container > div > div.gutter-condensed.gutter-lg.flex-column.flex-md-row.d-flex > div.flex-shrink-0.col-12.col-md-9.mb-4.mb-md-0 > div.Box.mb-3 > div.Box-header.Box-header--blue.position-relative > div > div.flex-1.d-flex.flex-items-center.ml-3.min-width-0 > div.d-flex.flex-auto.flex-justify-end.ml-3.flex-items-baseline > a:nth-child(3) > relative-time');
        }
        return element.getAttribute('datetime');
      });
    }

    async function getCommitsCount(page)
    {
      return await page.evaluate(() => {
        let element = document.querySelector('#repo-content-pjax-container > div > div.gutter-condensed.gutter-lg.flex-column.flex-md-row.d-flex > div.flex-shrink-0.col-12.col-md-9.mb-4.mb-md-0 > div.Box.mb-3 > div.Box-header.Box-header--blue.position-relative > div > div:nth-child(4) > ul > li > a > span > strong');
        return element.textContent;
      });
    }

    async function getBranchesCount(page)
    {
      return await page.evaluate(() => {
        let element = document.querySelector('#repo-content-pjax-container > div > div.gutter-condensed.gutter-lg.flex-column.flex-md-row.d-flex > div.flex-shrink-0.col-12.col-md-9.mb-4.mb-md-0 > div.file-navigation.mb-3.d-flex.flex-items-start > div.flex-self-center.ml-3.flex-self-stretch.d-none.d-lg-flex.flex-items-center.lh-condensed-ultra > a:nth-child(1) > strong');
        return element.textContent;
      });
    }

    var toScrap = await getRepositoriesToScrap();

    let repositories = toScrap.data.resource;
    if (repositories.length === 0)
    {
      console.log('No trending repositories found to scrap.');
      return;
    }

    const browser = await puppeteer.launch();
    const page = await browser.newPage();

    pages = repositories[0].repositories;

    for (let repoIdx = 0; repoIdx < pages.length; repoIdx++)
    {
      let solution = pages[repoIdx];

      let project = {
        url: solution,
      };
      const fs = require('fs');

      let solutionFile = solution
        .replaceAll('/', '')
        .replaceAll(':', '');
      const filePath = `project_${solutionFile}.json`;

      if (fs.existsSync(filePath))
      {
        console.log(`Project: ${solution} already scrapped, reading from file.`);
        let rawdata = fs.readFileSync(filePath);
        project = JSON.parse(rawdata);

        console.log(`Progress: [${repoIdx}/${pages.length}]`);
      }
      else
      {
        console.log(`Project: ${solution} not scrapped, scrapping.`);
        await page.goto(solution, {
          waitUntil: 'networkidle0',
        });

        //let license = await getLicense(page);
        console.log("Get stars");

        try
        {
          let starsCount = await getStars(page);

          console.log("Get about");
          let about = await getAbout(page);
  
          console.log("Get commits");
          let commits = await getCommitsCount(page);
  
          console.log("Get branches");
          let branches = await getBranchesCount(page);
  
          console.log("Get last commit date");
          let lastCommitDate = await getLastCommitDate(page);

          project.stars = starsCount;
          project.about = about;
          project.commits = commits;
          project.branches = branches;
          project.lastCommitDate = lastCommitDate;
          //project.license = license;
        }
        catch(error)
        {
          console.log(`Error during scrapping project data for project ${project.url}. Aborting.`)
          continue;
        }

        console.log(project);

        await page.click('#repo-content-pjax-container > div > div.gutter-condensed.gutter-lg.flex-column.flex-md-row.d-flex > div.flex-shrink-0.col-12.col-md-9.mb-4.mb-md-0 > div.file-navigation.mb-3.d-flex.flex-items-start > a');

        await page.waitForNavigation();
        await page.type('#tree-finder-field', '.csproj');
        await page.waitForTimeout(1000);

        let csprojs = await page.evaluate(() => {
            let data = [];
            let elements = document.getElementsByClassName('tree-browser-result');
            for (var element of elements)
            {
                var href = element.getAttribute('href');
                
                // cut some other files than .csproj
                if (href.endsWith('.csproj'))
                    data.push(href);
            }
            return data;
        });
        
        console.log(csprojs);
        project.csprojs = [];

        var parseString = require('xml2js').parseString;

        for (let i = 0; i < csprojs.length; i++)
        {
            const url = csprojs[i];
            console.log(`Progress: [${repoIdx}/${pages.length}] - ${i}/${csprojs.length}. Project: ${url}`);

            try
            {
                await page.goto(url);
                await page.click('#raw-url');

                let element = await page.$('pre')
                let csprojContent = await page.evaluate(el => el.textContent, element)

                let packages = [];
                parseString(csprojContent, function (err, result) {

                    try
                    {
                      const packageReferences = result.Project.ItemGroup.filter(p => p.PackageReference != null);

                      if (packageReferences.length === 0)
                      {
                        return;
                      }

                      packages = packageReferences[0].PackageReference
                        .filter(el => el.$.Include != null && el.$.Include !== '')
                        .map(el => ({ packageName: el.$.Include, packageVersion: el.$.Version }));
                    }
                    catch(error)
                    {
                      console.log(result);
                      throw error;
                    }
                });

                project.csprojs.push({
                    projectUrl: url,
                    packages: packages
                });
            }
            catch(error)
            {
                //console.log(error);
                project.csprojs.push({
                    projectUrl: url,
                    failed: true,
                    error: error,
                });
            }
        }

        fs.writeFileSync(filePath, JSON.stringify(project));

        //await page.screenshot({ path: 'example.png' });
      }

      // Send data
      axios.post(`${apiBaseUrl}`, project)
        .then(function (response) {
          console.log('Success!');
        })
        .catch(function (error) {
          console.log(error);
        });
    }

    await browser.close();
})();