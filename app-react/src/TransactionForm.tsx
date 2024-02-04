import React from "react";

const TransactionForm = () => {
  return (
    <div className="box">
      <div className="field">
        <label className="label">Account id</label>
        <input
          className="input"
          type="text"
          placeholder="00000000-0000-0000-0000-000000000000"
        />
      </div>

      <div className="field">
        <label className="label">Amount</label>
        <input className="input" type="text" />
      </div>

      <div className="field">
        <button className="button is-link">Create transaction</button>
      </div>
    </div>
  );
};

export { TransactionForm };
