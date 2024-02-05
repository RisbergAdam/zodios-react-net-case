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
          <b>Transaction history</b>
        </p>
        <progress className="progress is-primary" />
      </div>
    );
  }

  if (!transactionsQuery.data || !accountsQuery.data) {
    return (
      <div>
        <p className="mb-2">
          <b>Transaction history</b>
        </p>
        <div className="notification is-danger is-light">
          Could not fetch transactions, please try again later.
        </div>
      </div>
    );
  }

  const hasTransactions = transactionsQuery.data.length > 0;

  return (
    <div>
      <p className="mb-2">
        <b>Transaction history</b>
      </p>

      {!hasTransactions && (
        <>
          <div className="panel-block">
            <p className="has-text-grey-light">No transactions.</p>
          </div>
        </>
      )}

      {hasTransactions && (
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
                  Account:{" "}
                  <b className="is-family-monospace">{tx.account_id}</b>
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
      )}
    </div>
  );
};

export { TransactionList };
