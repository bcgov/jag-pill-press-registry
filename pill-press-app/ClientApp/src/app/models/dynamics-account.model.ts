import { DynamicsContact } from './dynamics-contact.model';

export class DynamicsAccount {
  id: string;
  bcIncorporationNumber: string;
  businessNumber: string;
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

  // related entities
  additionalContact: DynamicsContact;
  primaryContact: DynamicsContact;
}
