import { Budget } from "./components/budget/Budget";
import RepresentativePage from "./components/representative";

const AppRoutes = [
  {
    index: true,
    element: <Budget />
  },
  {
    path: '/representative',
    element: <RepresentativePage />
  }
];

export default AppRoutes;
