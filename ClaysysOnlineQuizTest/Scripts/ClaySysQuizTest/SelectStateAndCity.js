
document.addEventListener("DOMContentLoaded", function () {
    const stateCities = {
        "Tamil Nadu": [
            { value: "Coimbatore", text: "Coimbatore" },
            { value: "Chennai", text: "Chennai" },
            { value: "Madurai", text: "Madurai" }
        ],
        "Kerala": [
            { value: "Palakkad", text: "Palakkad" },
            { value: "Kochi", text: "Kochi" },
            { value: "Thiruvananthapuram", text: "Thiruvananthapuram" }
        ],
        "Karnataka": [
            { value: "Bengaluru", text: "Bengaluru" },
            { value: "Mysuru", text: "Mysuru" },
            { value: "Hubli", text: "Hubli" }
        ],
        "Maharashtra": [
            { value: "Mumbai", text: "Mumbai" },
            { value: "Pune", text: "Pune" },
            { value: "Nagpur", text: "Nagpur" }
        ],
        "Goa": [
            { value: "Panaji", text: "Panaji" },
            { value: "Margao", text: "Margao" },
            { value: "Vasco da Gama", text: "Vasco da Gama" }
        ]
    };

    const stateDropdown = document.getElementById('stateDropdown');
    const cityDropdown = document.getElementById('cityDropdown');

    stateDropdown.addEventListener('change', function () {
        const selectedState = this.value;
        const cities = stateCities[selectedState] || [];

        cityDropdown.innerHTML = '<option  value="">Select City</option>';
        cities.forEach(function (city) {
            const option = document.createElement('option');
            option.value = city.value;
            option.text = city.text;
            cityDropdown.appendChild(option);
        });
    });

    const initialState = stateDropdown.value;
    if (initialState) {
        const event = new Event('change');
        stateDropdown.dispatchEvent(event);
    }
});
