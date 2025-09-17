document.addEventListener("DOMContentLoaded", () => {
  const adviceTextEl = document.querySelector(".advice-text");
  const generateBtn = document.getElementById("btn");
  async function fetchAdvice() {
    try {
      const response = await fetch("https://api.adviceslip.com/advice", {
        cache: "no-cache",
      });
      const data = await response.json();
      const slip = data.slip;
      adviceTextEl.textContent = `"${slip.advice}"`;
    } catch (error) {
      adviceTextEl.textContent = "Error loading advice";
      console.error("Error fetching advice:", error);
    }
  }
  fetchAdvice();
  generateBtn.addEventListener("click", fetchAdvice);
});
