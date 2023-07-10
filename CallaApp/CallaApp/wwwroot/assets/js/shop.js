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




$(function () {


    //SORT
    $(document).on("change", "#sort", function (e) {
        e.preventDefault();
        let sortValue = $(this).val();
        let data = { sortValue: sortValue };
        let parent = $(".product-grid-view");

        $.ajax({
            url: "/Shop/Sort",
            type: "Get",
            data: data,
            success: function (res) {
                $(parent).html(res);

            }

        })
    })



    ////FILTER
    $(document).on("submit", "#filterForm", function (e) {
        e.preventDefault();
        let value1 = $(".min-price").val();
        let value2 = $(".max-price").val();
        let data = { value1: value1, value2: value2 }
        let parent = $(".product-grid-view");
        $.ajax({
            url: "/Shop/GetRangeProducts",
            type: "Get",
            data: data,
            success: function (res) {
                $(parent).html(res);
                if (value1 == "10" && value2 == "500") {
                    $(".shop-paginate").addClass("d-none")
                }
            }
        })
    })



})