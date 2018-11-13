import { browser, by, element } from 'protractor';

export class PillPressRetailLicensingPage {
    navigateTo() {
      return browser.get('/pillpress/policy-document/pill press-retail-licence');
    }

    getMainHeading() {
      return element(by.css('app-root h1')).getText();
    }   

}
