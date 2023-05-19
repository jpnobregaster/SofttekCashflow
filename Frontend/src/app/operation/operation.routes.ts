import { EntryComponent } from "./presentation/entry/entry.component";
import { HomeComponent } from "./presentation/home/home.component";
import { ReportComponent } from "./presentation/report/report.component";

type PathMatch = "full" | "prefix" | undefined;

export const OperationRoutes = [
	{ 
		path: '', 
		title: 'Home',
		redirectTo: 'home', 
		pathMatch: 'full' as PathMatch,
	},
	{
		path: 'home',
		component: HomeComponent,
	},
	{
		path: 'entry',
		component: EntryComponent,
	},
	{
		path: 'report',
		component: ReportComponent,
	},
];
