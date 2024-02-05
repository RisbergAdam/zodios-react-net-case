import React from "react";
import moxios from "moxios";
import { render, screen } from "@testing-library/react";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { TransactionList } from "./TransactionList";
import { api } from "./api";

const queryClient = new QueryClient({
  logger: {
    log: console.log,
    warn: console.warn,
    error: () => {},
  },
  defaultOptions: {
    queries: {
      retry: false,
    },
  },
});

const wrapper = (props: { children: React.ReactNode }) => (
  <QueryClientProvider client={queryClient}>
    {props.children}
  </QueryClientProvider>
);

describe("TransactionList", () => {
  beforeEach(() => {
    moxios.stubs.reset();
    queryClient.clear();
  });

  beforeAll(() => {
    moxios.install(api.axios);
  });

  afterAll(() => {
    moxios.uninstall(api.axios);
  });

  describe("when api returns no accounts or transactions", () => {
    it("should render no transactions", async () => {
      moxios.stubRequest("/transactions", {
        status: 200,
        response: [],
      });

      moxios.stubRequest("/accounts", {
        status: 200,
        response: [],
      });

      render(<TransactionList />, { wrapper });
      await screen.findByText(/No transactions/i);
    });
  });

  describe("when api returns errors", () => {
    it("should render the error dialog", async () => {
      moxios.stubRequest("/transactions", {
        status: 500,
        response: "Network error",
      });

      moxios.stubRequest("/accounts", {
        status: 500,
        response: "Network error",
      });

      render(<TransactionList />, { wrapper });
      await screen.findByText(
        /Could not fetch transactions, please try again later/i
      );
    });
  });

  describe("when api has accounts and transactions", () => {
    it("should render a list of transactions", async () => {
      moxios.stubRequest("/transactions", {
        status: 200,
        response: [
          {
            transaction_id: "ec326214-957c-4014-a210-55f93996dffd",
            account_id: "2ef41f70-364b-4e09-8184-7d97ad6c507c",
            amount: 1500,
            created_at: "2024-02-05T09:38:24Z",
          },
          {
            transaction_id: "35a877bb-52dc-434d-8825-84cbd4cac9a4",
            account_id: "2ef41f70-364b-4e09-8184-7d97ad6c507c",
            amount: 500,
            created_at: "2024-02-05T09:38:24Z",
          },
        ],
      });

      moxios.stubRequest("/accounts", {
        status: 200,
        response: [
          {
            account_id: "2ef41f70-364b-4e09-8184-7d97ad6c507c",
            balance: 2000,
          },
        ],
      });

      render(<TransactionList />, { wrapper });

      const tx1 = await screen.findByTestId(
        "transaction-ec326214-957c-4014-a210-55f93996dffd"
      );

      const tx2 = await screen.findByTestId(
        "transaction-35a877bb-52dc-434d-8825-84cbd4cac9a4"
      );

      // assert first transaction

      expect(tx1).toHaveTextContent("1500$");
      expect(tx1).toHaveTextContent(
        "Account: 2ef41f70-364b-4e09-8184-7d97ad6c507c"
      );
      expect(tx1).toHaveTextContent("Current account balance: 2000$");

      // assert second transaction

      expect(tx2).toHaveTextContent("500$");
      expect(tx2).toHaveTextContent(
        "Account: 2ef41f70-364b-4e09-8184-7d97ad6c507c"
      );
      expect(tx2).not.toHaveTextContent("Current account balance: 2000$");
    });
  });
});
