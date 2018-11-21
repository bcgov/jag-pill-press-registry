import { DynamicsContact } from './dynamics-contact.model';
import { CustomAddress } from './custom-address.model';

export class DynamicsAccount {
  id: string;

  bcIncorporationNumber: string;
  businessDBAName: string;
  businessEmail: string;
  consentForEmailCommunication: boolean;
  businessLegalName: string;
  businessNumber: string;
  businessPhoneNumber: string;
  businessType: string;
  contactEmail: string;
  contactPhone: string;
  dateOfIncorporationInBC: Date;
  description: string;

  physicalAddressLine1: string;
  physicalAddressLine2: string;
  physicalAddressCity: string;
  physicalAddressPostalCode: string;
  physicalAddressProvince: string;
  physicalAddressCountry: string;
  mailingAddressLine1: string;
  mailingAddressLine2: string;
  mailingAddressCity: string;
  mailingAddressPostalCode: string;
  mailingAddressProvince: string;
  mailingAddressCountry: string;

  name: string;
  pstNumber: string;
  websiteAddress: string;

  // related entities
  additionalContact: DynamicsContact;
  mailingAddress: CustomAddress;
  physicalAddress: CustomAddress;
  primaryContact: DynamicsContact;
}
