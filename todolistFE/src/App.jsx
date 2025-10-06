import { useState, useEffect } from "react";
import Header from "./components/Header";
import TodoForm from "./components/TodoForm";
import TodoList from "./components/TodoList";

function App() {
  const [todos, setTodos] = useState([]);

const API_TODOS = "/api/todos";
console.log("Using API URL:", API_TODOS);

  console.log("Using API URL:", API_TODOS);

  useEffect(() => {
    const fetchTodoList = async () => {
      try {
        const response = await fetch(API_TODOS);
        const data = await response.json();
        setTodos(data);
      } catch (err) {
        console.error("Fel vid hämtning:", err);
      }
    };
    fetchTodoList();
  }, [API_TODOS]);

  const addTodo = async (title, description) => {
    const newTodo = {
      Title: title.trim(),
      Description: description,
      IsDone: false,
    };

    try {
      const res = await fetch(API_TODOS, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(newTodo),
      });
      const savedTodo = await res.json();
      setTodos([...todos, savedTodo]);
    } catch (err) {
      console.error("Fel vid skapande:", err);
    }
  };

  const toggleTodo = async (id) => {
    const todo = todos.find((t) => t.id === id);
    if (!todo) return;

    const updatedIsDone = !todo.isDone;

    try {
      const res = await fetch(
        `${API_TODOS}/${id}/done?isDone=${updatedIsDone}`,
        {
          method: "PATCH",
        }
      );
      const updatedTodo = await res.json();
      setTodos(todos.map((t) => (t.id === id ? updatedTodo : t)));
    } catch (err) {
      console.error("Fel vid uppdatering:", err);
    }
  };

  const deleteTodo = async (id) => {
    try {
      await fetch(`${API_TODOS}/${id}`, {
        method: "DELETE",
      });
      setTodos(todos.filter((t) => t.id !== id));
    } catch (err) {
      console.error("Fel vid borttagning:", err);
    }
  };

  return (
    <div className="max-w-md mx-auto mt-8 p-6 bg-purple-200 rounded-xl shadow-md">
      <Header />
      <TodoForm onAddTodo={addTodo} />
      <TodoList
        todos={todos}
        onToggleTodo={toggleTodo}
        onDeleteTodo={deleteTodo}
      />
    </div>
  );
}

export default App;
