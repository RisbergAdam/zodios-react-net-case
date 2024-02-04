import React from "react";
import ReactDOM from "react-dom/client";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";

import "bulma/css/bulma.min.css";

import { TransactionForm } from "./TransactionForm";
import { TransactionList } from "./TransactionList";

const queryClient = new QueryClient();

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);

root.render(
  <React.StrictMode>
    <QueryClientProvider client={queryClient}>
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
    </QueryClientProvider>
  </React.StrictMode>
);
