
export interface CreateRefundResponce {
  refund: Refund;
  success: boolean;
}

export interface Failure {
  reason?: string;
  message?: string;
}

export interface GetPaymentDetails {
  id?: string;
  title?: string;
  payment_type?: string;
  payment_request?: string;
  status: boolean;
  link: object;
  product: object;
  seller?: string;
  currency?: string;
  amount?: string;
  name?: string;
  email?: string;
  phone?: string;
  payout: object;
  fees: object;
  total_taxes: object;
  cases: object[];
  affiliate_id: object;
  affiliate_commission: object;
  instrument_type?: string;
  billing_instrument?: string;
  failure: Failure;
  created_at?: string;
  updated_at?: string;
  tax_invoice_id?: string;
  resource_uri?: string;
}

export interface Instamojo {
}

export interface Refund {
  id?: string;
  payment_id?: string;
  status?: string;
  type?: string;
  body?: string;
  refund_amount?: number;
  total_amount?: number;
  created_at?: string;
}
