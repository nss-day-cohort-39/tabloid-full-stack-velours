import React from "react";
import { BrowserRouter as Router } from "react-router-dom";
import { UserProfileProvider } from "./providers/UserProfileProvider";
import Header from "./components/Header";
import ApplicationViews from "./components/ApplicationViews";
import { CategoryProvider } from "./providers/CategoryProvider";
import { PostProvider } from "./providers/PostProvider";
import { TagProvider } from "./providers/TagProvider";
import { CommentProvider } from './providers/CommentProvider';

function App() {
  return (
    <Router>
      <UserProfileProvider>
        <PostProvider>
          <CommentProvider>
          <CategoryProvider>
            <TagProvider>
              <Header />
              <ApplicationViews />
            </TagProvider>
          </CategoryProvider>
          </CommentProvider>
        </PostProvider>
      </UserProfileProvider>
    </Router>
  );
}

export default App;
