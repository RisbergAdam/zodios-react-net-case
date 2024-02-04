import React from "react";
import { apiHooks } from "./api";

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
            <a
              className="panel-block is-flex-wrap-wrap"
              key={tx.transaction_id}
              href="/#"
            >
              <p>
                Account: <b className="is-family-monospace">{tx.account_id}</b>
              </p>

              <p className="ml-auto">
                <b className="is-family-monospace">{tx.amount}$</b>
              </p>

              {isFirst && (
                <>
                  <div style={{ flexBasis: "100% " }} />
                  <p className="has-text-info">
                    Current account balance:{" "}
                    <b className="is-family-monospace">{account?.balance}$</b>
                  </p>
                </>
              )}
            </a>
          );
        })}
      </nav>
    </div>
  );
};

export { TransactionList };
