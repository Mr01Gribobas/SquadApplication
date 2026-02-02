import React, { useState } from "react";
import logo from "./logo.svg";
import "../src/App.css";
import { BrowserRouter } from "react-router-dom";

function App() {
  return (
    <div>
      <BrowserRouter>
        <TestComponent></TestComponent>
      </BrowserRouter>
    </div>
  );
}
function TestComponent() {
  const [time, setTime] = useState(new Date().toLocaleTimeString());
  setInterval(() => {
    setTime(new Date().toLocaleTimeString());
  }, 1000);
  return (
    <div className="stalker-pda">
      {/* Статус бар ПДА */}
      <div className="pda-status-bar">
        <div className="pda-time">{time}</div>
        <div className="pda-battery">
          <div className="battery-level"></div>
        </div>
      </div>

      {/* Основной контент */}
      <div className="pda-screen">
        {/* Заголовок с логотипом */}
        <header className="pda-header">
          <div className="logo">
            <span className="logo-text">S.T.R.I.K.E.R</span>
            <span className="logo-subtext">TEAM CONTROL SYSTEM v1.3</span>
          </div>
          <div className="user-info">
            <div className="user-name">Сталкер: [Позывной]</div>
            <div className="user-rank">Ранг: Новичок</div>
          </div>
        </header>

        {/* Навигация в стиле ПДА */}
        <nav className="pda-navigation">
          <button className="nav-btn active">
            <span className="nav-icon">🎮</span>
            <span className="nav-text">События</span>
          </button>
          <button className="nav-btn">
            <span className="nav-icon">👥</span>
            <span className="nav-text">Команда</span>
          </button>
          <button className="nav-btn">
            <span className="nav-icon">🎒</span>
            <span className="nav-text">Снаряжение</span>
          </button>
          <button className="nav-btn">
            <span className="nav-icon">🗺️</span>
            <span className="nav-text">Полигоны</span>
          </button>
          <button className="nav-btn">
            <span className="nav-icon">📡</span>
            <span className="nav-text">Сообщения</span>
          </button>
        </nav>

        {/* Основной контент */}
        <main className="pda-content">
          <div className="content-section">
            <h2 className="section-title">
              <span className="title-text">АКТИВНЫЕ ЗАДАНИЯ</span>
              <span className="title-badge">3</span>
            </h2>

            <div className="missions-list">
              <div className="mission-card">
                <div className="mission-header">
                  <span className="mission-type">⚔️ БОЕВАЯ</span>
                  <span className="mission-time">19:00</span>
                </div>
                <h3 className="mission-title">
                  Зачистка "Заброшенного завода"
                </h3>
                <p className="mission-description">
                  Встреча с командой "Ренегаты" на территории заброшенного
                  химического завода
                </p>
                <div className="mission-details">
                  <span>📍 Полигон: "Химзавод"</span>
                  <span>👥 Участников: 8/12</span>
                </div>
              </div>

              <div className="mission-card">
                <div className="mission-header">
                  <span className="mission-type">🏃 ТРЕНИРОВКА</span>
                  <span className="mission-time">Завтра 15:00</span>
                </div>
                <h3 className="mission-title">Стрелковые учения</h3>
                <p className="mission-description">
                  Тренировка точности и координации на полигоне "Стрельбище"
                </p>
                <div className="mission-details">
                  <span>📍 Полигон: "Стрельбище"</span>
                  <span>👥 Участников: 5/10</span>
                </div>
              </div>
            </div>
          </div>

          <div className="content-section">
            <h2 className="section-title">
              <span className="title-text">СТАТУС КОМАНДЫ</span>
            </h2>

            <div className="team-status">
              <div className="status-item">
                <span className="status-label">Всего сталкеров:</span>
                <span className="status-value">24</span>
              </div>
              <div className="status-item">
                <span className="status-label">В сети:</span>
                <span className="status-value online">18</span>
              </div>
              <div className="status-item">
                <span className="status-label">На заданиях:</span>
                <span className="status-value">6</span>
              </div>
              <div className="status-item">
                <span className="status-label">Аномалий обнаружено:</span>
                <span className="status-value">3</span>
              </div>
            </div>
          </div>
        </main>

        {/* Системные сообщения */}
        <div className="system-messages">
          <div className="system-alert">
            <span className="alert-icon">⚠️</span>
            <span className="alert-text">Выброс через: 4ч 23м</span>
          </div>
          <div className="system-alert">
            <span className="alert-icon">📶</span>
            <span className="alert-text">Связь: УСТАНОВЛЕНА</span>
          </div>
        </div>
      </div>

      {/* Нижняя панель управления */}
      <div className="pda-controls">
        <button className="control-btn">МЕНЮ</button>
        <button className="control-btn">ВЫБОР</button>
        <button className="control-btn">НАЗАД</button>
        <button className="control-btn">СКАН</button>
      </div>
    </div>
  );
}
export default App;
