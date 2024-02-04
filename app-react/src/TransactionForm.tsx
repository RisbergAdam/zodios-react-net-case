import React from "react";
import clsx from "clsx";
import { useQueryClient } from "@tanstack/react-query";
import { z } from "zod";
import { Formik } from "formik";
import { toFormikValidationSchema } from "zod-formik-adapter";

import { apiHooks } from "./api";

const formSchema = toFormikValidationSchema(
  z.object({
    account_id: z.string().uuid({
      message: "Account must be a valid UUID",
    }),
    amount: z.coerce.number({
      invalid_type_error: "Amount must be a number",
    }),
  })
);

const TransactionForm = () => {
  const queryClient = useQueryClient();
  const createTransaction = apiHooks.useCreateTransaction();

  return (
    <div className="box">
      <Formik
        initialValues={{ account_id: "", amount: "" }}
        validationSchema={formSchema}
        onSubmit={(values, form) =>
          createTransaction.mutate(
            {
              account_id: values.account_id,
              amount: parseInt(values.amount),
            },
            {
              onSuccess: () => {
                queryClient.invalidateQueries();
                form.resetForm();
              },
            }
          )
        }
      >
        {(form) => (
          <form onSubmit={form.handleSubmit}>
            <div className="field">
              <label className="label">Account</label>
              <input
                type="text"
                name="account_id"
                className="input"
                placeholder="00000000-0000-0000-0000-000000000000"
                onChange={form.handleChange}
                onBlur={form.handleBlur}
                value={form.values.account_id}
              />
              {form.errors.account_id && form.touched.account_id && (
                <p className="help is-danger">{form.errors.account_id}</p>
              )}
            </div>

            <div className="field">
              <label className="label">Amount</label>
              <input
                type="text"
                name="amount"
                className="input"
                onChange={form.handleChange}
                onBlur={form.handleBlur}
                value={form.values.amount}
              />
              {form.errors.amount && form.touched.amount && (
                <p className="help is-danger">{form.errors.amount}</p>
              )}
            </div>

            <div className="field">
              <button
                className={clsx(
                  "button",
                  "is-link",
                  createTransaction.isLoading && "is-loading"
                )}
                disabled={!form.isValid}
              >
                Create transaction
              </button>
            </div>
          </form>
        )}
      </Formik>
    </div>
  );
};

export { TransactionForm };
