import { browser, by, element } from 'protractor';
import { PillPressRetailLicensingPage } from './pillpress-retail-licensing.po';

const VOTE_SLUG = 'initialNumberInterested';

describe('Policy Content feature test', () => {
  let page: PillPressRetailLicensingPage;

  beforeEach(() => {
    page = new PillPressRetailLicensingPage();
  });

  it('Pill Press Retail Licence', () => {
    page.navigateTo();
    expect(page.getMainHeading()).toEqual("Pill Press Retail Licence");   
  });
});
