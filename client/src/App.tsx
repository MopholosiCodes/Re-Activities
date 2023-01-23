import React,{useState, useEffect} from 'react';
import axios from "axios";
import logo from './logo.svg';
import './App.css';

function App() {
  const [activities, setActivities] = useState([]);

  useEffect(() => {
    axios.get("https://localhost:5000/api/Activities")
      .then(response => {
        console.table(response.data)
        setActivities(response.data)
      })
  },[])

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <ul>
          {activities.map((activity: any) => (
            <li key={activity.id}>{activity.title}</li>
          ))}
        </ul>
      </header>
    </div>
  );
}

export default App;
