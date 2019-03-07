export class Certificate {
  id: string;
  name: string;
  approvedIntendedUse: string;
  issueDate: Date;
  issueDateString: string;
  expiryDate: Date;
  expiryDateString: string;

  hasCertificate: boolean; // used to indicate that the certificate exists in sharepoint (not returned by API)
  hasExpired: boolean; // (not returned by API)
}
