import Constants from "./constants";
import DashboardComponent from "./dashboard";
import TransactionList from "./transaction-list";
const components = { };

components[Constants.DashboardComponent] = DashboardComponent;
components[Constants.TransactionList] = TransactionList;

export default components;