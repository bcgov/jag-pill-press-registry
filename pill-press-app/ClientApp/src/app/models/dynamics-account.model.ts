import { DynamicsContact } from './dynamics-contact.model';
import { CustomAddress } from './custom-address.model';

export class DynamicsAccount {
  id: string;
  bcIncorporationNumber: string;
  businessDBAName: string;
  businessEmail: string;
  businessLegalName: string;
  businessNumber: string;
  businessPhoneNumber: string;
  businessType: string;
  consentForEmailCommunication: boolean;
  contactEmail: string;
  contactPhone: string;
  dateOfIncorporationInBC: Date;
  declarationofcorrectinformation: boolean;
  description: string;
  foippaconsent: boolean;
  mailingAddressCity: string;
  mailingAddressCountry: string;
  mailingAddressLine1: string;
  mailingAddressLine2: string;
  mailingAddressPostalCode: string;
  mailingAddressProvince: string;
  name: string;
  physicalAddressCity: string;
  physicalAddressCountry: string;
  physicalAddressLine1: string;
  physicalAddressLine2: string;
  physicalAddressPostalCode: string;
  physicalAddressProvince: string;
  pstNumber: string;
  submittedDate: Date;
  websiteAddress: string;

  // related entities
  additionalContact: DynamicsContact;
  mailingAddress: CustomAddress;
  physicalAddress: CustomAddress;
  primaryContact: DynamicsContact;
}
