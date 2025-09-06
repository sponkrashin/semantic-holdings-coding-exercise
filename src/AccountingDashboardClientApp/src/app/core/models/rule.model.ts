export interface Rule {
  id: number;
  client: string;
  program: string;
  depositDestination: string; // e.g., 'PayPal …1234'
  updatedDate: string; // ISO date string
}
