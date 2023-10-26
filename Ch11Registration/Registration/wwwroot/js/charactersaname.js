// Add custom validation method to check if a string does not end with "Patrick"
jQuery.validator.addMethod("charactersaname", function (value, element, param) {
    console.log("It reaches the validation function");

    if (!value) return false;  // Return false if value is empty

    return !value.endsWith("Patrick");  // Return false if it ends with "Patrick", otherwise true
});

// Add the custom validator to the unobtrusive adapters
jQuery.validator.unobtrusive.adapters.addSingleVal("charactersaname");
