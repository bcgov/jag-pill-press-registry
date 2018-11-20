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
  mailingAddresPostalCode: string;
  mailingAddressCity: string;
  mailingAddressCountry: string;
  mailingAddressName: string;
  mailingAddressProvince: string;
  mailingAddressStreet: string;
  name: string;
  pstNumber: string;
  websiteAddress: string;

  // related entities
  additionalContact: DynamicsContact;
  mailingAddress: CustomAddress;
  physicalAddress: CustomAddress;
  primaryContact: DynamicsContact;
}
