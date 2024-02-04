import { z } from "zod";
import { Zodios } from "@zodios/core";
import { ZodiosHooks } from "@zodios/react";

const Transaction = z.object({
  transaction_id: z.string(),
  account_id: z.string(),
  amount: z.number(),
  created_at: z.string(),
});

const Account = z.object({
  account_id: z.string(),
  balance: z.number(),
});

const api = new Zodios("http://localhost:5000/", [
  {
    method: "get",
    path: "/transactions",
    alias: "listTransactions",
    response: z.array(Transaction),
  },
  {
    method: "get",
    path: "/transactions/:id",
    alias: "getTransaction",
    response: Transaction.optional(),
  },
  {
    method: "post",
    path: "/transactions",
    alias: "createTransaction",
    response: Transaction,
  },
  {
    method: "get",
    path: "/accounts",
    alias: "listAccounts",
    response: z.array(Account),
  },
]);

export const apiHooks = new ZodiosHooks("ACCOUNTING_API", api);
