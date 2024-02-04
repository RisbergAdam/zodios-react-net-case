import React from "react";
import { apiHooks } from "./api";

export const formatCurrency = (amount: number) => `${amount}$`;

const TransactionList = () => {
  const transactionsQuery = apiHooks.useListTransactions();
  const accountsQuery = apiHooks.useListAccounts();

  if (transactionsQuery.isLoading || accountsQuery.isLoading) {
    return (
      <div>
        <p className="mb-2">
          <b>Transaction history loading</b>
        </p>
      </div>
    );
  }

  if (!transactionsQuery.data || !accountsQuery.data) {
    return (
      <div>
        <p className="mb-2">
          <b>Transaction history error</b>
        </p>
      </div>
    );
  }

  return (
    <div>
      <p className="mb-2">
        <b>Transaction history</b>
      </p>
      <nav className="panel">
        {transactionsQuery.data.map((tx, i) => {
          const isFirst = i === 0;
          const account = accountsQuery.data.find(
            (acc) => acc.account_id === tx.account_id
          );

          return (
            <div
              className="panel-block is-flex-wrap-wrap"
              data-type="transaction"
              data-account-id={tx.account_id}
              data-amount={tx.amount}
              data-balance={account?.balance ?? 0}
              key={tx.transaction_id}
            >
              <p>
                Account: <b className="is-family-monospace">{tx.account_id}</b>
              </p>

              <p className="ml-auto">
                <b className="is-family-monospace">
                  {formatCurrency(tx.amount)}
                </b>
              </p>

              {isFirst && account && (
                <>
                  <div style={{ flexBasis: "100% " }} />
                  <p className="has-text-info">
                    Current account balance:{" "}
                    <b className="is-family-monospace">
                      {formatCurrency(account.balance)}
                    </b>
                  </p>
                </>
              )}
            </div>
          );
        })}
      </nav>
    </div>
  );
};

export { TransactionList };
