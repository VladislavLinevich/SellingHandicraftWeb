var seeBtn = document.getElementById("more");
var more_categories = document.getElementById("additional-categories");
var additional_categories = document.querySelectorAll("#additional-categories a")

console.log()
const seeMore = function () {
    if (more_categories.style.maxHeight == `${230 * (Math.floor((additional_categories.length - 1) / 6) + 1)}px`) {
        more_categories.style.maxHeight = "0px";
        more_categories.style.paddingBottom = "0px";
        seeBtn.style.marginTop = "0px";
        seeBtn.innerHTML = "Показать больше"; 
    } else {
        more_categories.style.maxHeight = `${230 * (Math.floor((additional_categories.length - 1) / 6) + 1)}px`;
        more_categories.style.paddingBottom = "30px";
        seeBtn.innerHTML = "Показать меньше"; 
    }
 };

 seeBtn.addEventListener("click", function (e) {
    e.stopPropagation();
    seeMore();
  });