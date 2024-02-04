import React from "react";

const TransactionList = () => {
  const transactions = [
    {
      transactionId: "06400aac-0646-4ba8-9aa5-6d4acaac0583",
      accountId: "7c5b3b16-a029-4161-96d0-356ad527517d",
      amount: 1000,
      createdAt: "2024-02-04T17:43:51Z",
    },
    {
      transactionId: "06400aac-0646-4ba8-9aa5-6d4acaac0583",
      accountId: "7c5b3b16-a029-4161-96d0-356ad527517d",
      amount: 1000,
      createdAt: "2024-02-04T17:43:51Z",
    },
    {
      transactionId: "06400aac-0646-4ba8-9aa5-6d4acaac0583",
      accountId: "7c5b3b16-a029-4161-96d0-356ad527517d",
      amount: 1000,
      createdAt: "2024-02-04T17:43:51Z",
    },
  ];

  return (
    <div>
      <p className="mb-2">
        <b>Transaction history</b>
      </p>

      <nav className="panel">
        {transactions.map((tx) => (
          <a className="panel-block is-flex-wrap-wrap">
            <p>
              Account: <b className="is-family-monospace">{tx.accountId}</b>
            </p>

            <p className="ml-auto">
              <b className="is-family-monospace">{tx.amount}$</b>
            </p>

            <div style={{ flexBasis: "100% " }} />
            <p className="has-text-info">
              Current account balance:{" "}
              <b className="is-family-monospace">1000$</b>
            </p>
          </a>
        ))}
      </nav>
    </div>
  );
};

export { TransactionList };
