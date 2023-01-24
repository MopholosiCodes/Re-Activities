import React,{useState, useEffect} from 'react';
import axios from "axios";
import './App.css';
import { Header } from 'semantic-ui-react';

function App() {
  const [activities, setActivities] = useState([]);

  useEffect(() => {
    axios
      .get("https://localhost:5000/api/Activities")
      .then(response => setActivities(response.data))
  },[])

  return (
    <div>
      <Header as="h1" icon='users' content="Re-Activities" />
      <ul>
          {activities.map((activity: any) => (
            <li key={activity.id}>{activity.title}</li>
          ))}
        </ul>
    </div>
  );
}

export default App;
