import './App.css';
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import Link from './pages/home/link';
import Repository from './pages/home/repository';
import Blog from './pages/blog';
import About from './pages/home/about';
import Ide from './pages/ide';
import Manage from './pages/manage';
import Login from './pages/login';
import ManageHome from './pages/manage/home';
import ManageBlog from './pages/manage/blog';
import PushBlog from './pages/manage/push-blog';
import Classify from './pages/manage/classify';
import Compilations from './pages/home/compilations';
import Index from './pages/home';
import Home from './pages/home/home';
import ResourceList from './pages/home/resource-list';
import ResourceListManage from './pages/manage/resource-list';

const router = createBrowserRouter([
  {
    path: "/",
    element: <Index />,
    children: [
      {
        path: "/",
        element: <Home />,
      },
      {
        path: "/links",
        element: <Link />,
      },
      {
        path: "/repository",
        element: <Repository />,
      },
      {
        path: "/about",
        element: <About />,
      },
      {
        path: '/compilations',
        element: <Compilations />
      }, {
        path: "/resource-list",
        element: <ResourceList />
      }
    ]
  }, {
    path: "/blog",
    element: <Blog />,
  }, {
    path: "/web-ide",
    element: <Ide />,
  }, {
    path: "/manage",
    element: <Manage />,
    children: [
      {
        path: "/manage",
        element: <ManageHome />,
      }, {
        path: "/manage/blog",
        element: <ManageBlog />,
      },  {
        path: "/manage/resource-list",
        element: <ResourceListManage />,
      }, {
        path: "/manage/push-blog",
        element: <PushBlog />,
      }, {
        path: "/manage/classify",
        element: <Classify />,
      }
    ]
  }, {
    path: "/login",
    element: <Login />
  }
]);

function App() {
  return (
    <RouterProvider router={router} />

  );
}

export default App;
