import Constants from "./constants";
import DashboardComponent from "./dashboard";
import TransactionList from "./transaction-list";
import DataPager from "./data-pager";
import BudgetStatistics from "./budget-statistics";
import AjaxDialog from "./ajax-dialog";
import TagsInput from "./tags-input";
import NotificationBar from "./notification-bar";

const components = { };

components[Constants.DashboardComponent] = DashboardComponent;
components[Constants.TransactionList] = TransactionList;
components[Constants.DataPager] = DataPager;
components[Constants.BudgetStatistics] = BudgetStatistics;
components[Constants.AjaxDialog] = AjaxDialog;
components[Constants.TagsInput] = TagsInput;
components[Constants.NotificationBar] = NotificationBar;

export default components;