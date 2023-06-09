
import Home from "./components/Home.js";
import RankItemsContainer from "./components/RankItemsContainer.js";
import { ChakraProvider } from "@chakra-ui/react";


const AppRoutes = [
  {
    index: true,
    element: <ChakraProvider><Home /></ChakraProvider>
  },
    {
        path: '/rank-movies',
        element: <RankItemsContainer dataType={"movie"}  />
    },
    {
        path: '/rank-albums',
        element: <RankItemsContainer dataType={"album"}  />
    }
];

export default AppRoutes;