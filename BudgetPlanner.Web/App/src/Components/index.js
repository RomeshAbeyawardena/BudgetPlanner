import Constants from "./constants";
import DashboardComponent from "./dashboard";
import TransactionList from "./transaction-list";
import DataPager from "./data-pager";
import BudgetStatistics from "./budget-statistics";
const components = { };

components[Constants.DashboardComponent] = DashboardComponent;
components[Constants.TransactionList] = TransactionList;
components[Constants.DataPager] = DataPager;
components[Constants.BudgetStatistics] = BudgetStatistics;

export default components;