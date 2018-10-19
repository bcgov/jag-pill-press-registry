import { browser, by, element } from 'protractor';
import { Pill PressRetailLicensingPage } from './cannabis-retail-licensing.po';

const VOTE_SLUG = 'initialNumberInterested';

describe('Policy Content feature test', () => {
  let page: Pill PressRetailLicensingPage;

  beforeEach(() => {
    page = new Pill PressRetailLicensingPage();
  });

  it('Pill Press Retail Licence', () => {
    page.navigateTo();
    expect(page.getMainHeading()).toEqual("Pill Press Retail Licence");   
  });
});
