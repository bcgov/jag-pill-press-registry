import { browser, by, element } from 'protractor';
import { AppHomePage } from './app.po';

describe('App Home Page', () => {
    let page: AppHomePage;

    beforeEach(() => {
        page = new AppHomePage();
    });

    it('should display a title', async () => {
        await page.navigateTo();
        expect(await page.getMainHeading()).toEqual('Register to own or sell a Pill Press');
    });



});
