let minValue = document.getElementById("min-value");
// console.log(minValue);
let maxValue = document.getElementById("max-value");

function validateRange(minPrice, maxPrice) {
  if (minPrice > maxPrice) {
    // Swap to Values
    let tempValue = maxPrice;
    maxPrice = minPrice;
    minPrice = tempValue;
  }

  minValue.innerHTML = "$" + minPrice;
  maxValue.innerHTML = "$" + maxPrice;
}

const inputElements = document.querySelectorAll(".range-slider input");
inputElements.forEach((element) => {
  element.addEventListener("change", (e) => {
    let minPrice = parseInt(inputElements[0].value);
    let maxPrice = parseInt(inputElements[1].value);

    validateRange(minPrice, maxPrice);
  });
});

validateRange(inputElements[0].value, inputElements[1].value);



// Sort

$(function () {

    $(".form-select").on("change", function () {
        var value = $(this).val();
        var data = { value: value };
        var url = "/Shop/Sort";
        var parent = $(".product-grid-view");

        $.ajax({
            type: "GET",
            url: url,
            data: data,
            success: function (res) {
                parent.html(res);
            }
        });
        return false;
    });



    ////FILTER
    $(document).on("submit", "#filterForm", function (e) {
        e.preventDefault();
        let value1 = $(".min-price")
        let value2 = $(".max-price")
        console.log(value1)
        console.log(value2)

        let data = { value1: value1, value2: value2 }
        let parent = $(".product-grid-view");

        $.ajax({
            url: "/Shop/Index",
            type: "Get",
            data: data,
            success: function (res) {
                $(".product-grid-view").html(res);
            }
        })
    })



})