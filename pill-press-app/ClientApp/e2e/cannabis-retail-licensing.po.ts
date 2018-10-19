import { browser, by, element } from 'protractor';

export class Pill PressRetailLicensingPage {
    navigateTo() {
      return browser.get('/pillpress/policy-document/cannabis-retail-licence');
    }

    getMainHeading() {
      return element(by.css('app-root h1')).getText();
    }   

}
