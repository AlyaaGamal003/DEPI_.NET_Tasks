document.addEventListener("DOMContentLoaded", () => {
  const adviceIdEl = document.querySelector(".advice-id");
  const adviceTextEl = document.querySelector(".advice-text");
  const generateBtn = document.getElementById("generateBtn");
  async function fetchAdvice() {
    try {
      const response = await fetch("https://api.adviceslip.com/advice", {
        cache: "no-cache",
      });
      const data = await response.json();
      const slip = data.slip;

      adviceIdEl.textContent = `Advice #${slip.id}`;
      adviceTextEl.textContent = `"${slip.advice}"`;
    } catch (error) {
      adviceIdEl.textContent = "Error";
      adviceTextEl.textContent = "حدث خطأ في تحميل النصيحة 😢";
      console.error("Error fetching advice:", error);
    }
  }
  fetchAdvice();
  generateBtn.addEventListener("click", fetchAdvice);
});
