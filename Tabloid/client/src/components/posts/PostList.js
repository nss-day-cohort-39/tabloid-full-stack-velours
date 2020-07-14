import React, { useContext, useEffect } from "react"
import { PostContext } from "../../providers/PostProvider";
import Post from "./Post";
import {CardColumns, Button} from "reactstrap"
import { useHistory } from "react-router-dom";
import "./Post.css"

const PostList = () => {
    const { posts, getAllPosts} = useContext(PostContext);
    const history = useHistory();

    useEffect(() => {
        getAllPosts();
        // eslint-disable-next-line 
      }, []);

    const handleClick = () => {
        history.push(`/addposts`);
    }

    return (
        <>
            <section>
                <div className="postsHeader">
                    <h2>Posts</h2>
                </div>
                <div className="btn--addPost">
                    <Button color="info" size="lg" onClick={handleClick}>Add Post</Button>
                </div>
                <CardColumns>
                    {
                        posts.map(post => {
                            return <Post post={post} />
                        })
                    }
                </CardColumns>
            </section>
        </>
    )
}

export default PostList