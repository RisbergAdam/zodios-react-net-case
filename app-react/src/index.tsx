import React from "react";
import ReactDOM from "react-dom/client";

import "bulma/css/bulma.min.css";

import { TransactionForm } from "./TransactionForm";
import { TransactionList } from "./TransactionList";

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <div className="container">
      <h1 className="title my-4">Accounting system</h1>
      <div className="columns">
        <div className="column is-4">
          <TransactionForm />
        </div>
        <div className="column is-8">
          <TransactionList />
        </div>
      </div>
    </div>
  </React.StrictMode>
);
