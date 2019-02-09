//Custom JS
//==================================================
jQuery(function ($) {
    var App = {
        $placeholder: $('[placeholder]'),
        $quoteLink: $('#lnkGetQuote'),
        $navbarToggle: $('.navbar-toggle'),
        bindEvents: function () {
            $(document).on('ready', this.docReady());
        },
        docReady: function () {
            this.$navbarToggle.click(function () {
                $(this).toggleClass("active");
            });

            //Smooth scroll
            this.$quoteLink.click(function () {
                var $anchor = $(this);
                $('html, body').stop().animate({ scrollTop: $($anchor.attr('href')).offset().top }, 1000);
                return false;
            });

            //Modernizr
            if (!Modernizr.input.placeholder) {

                this.$placeholder.focus(function () {
                    var input = $(this);
                    if (input.val() == input.attr('placeholder')) {
                        input.val('');
                        input.removeClass('placeholder');
                    }
                }).blur(function () {
                    var input = $(this);
                    if (input.val() == '' || input.val() == input.attr('placeholder')) {
                        input.addClass('placeholder');
                        input.val(input.attr('placeholder'));
                    }
                }).blur();
                this.$placeholder.parents('form').submit(function () {
                    $(this).find('[placeholder]').each(function () {
                        var input = $(this);
                        if (input.val() == input.attr('placeholder')) {
                            input.val('');
                        }
                    })
                });
            };
        },
        init: function () {
            this.bindEvents();
        }
    };

    App.init();
});

//Google-Analytics
//==================================================
//(function (i, s, o, g, r, a, m) {
//    i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () { (i[r].q = i[r].q || []).push(arguments) }
//        , i[r].l = 1 * new Date(); a = s.createElement(o),
//            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
//})(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');
//ga('create', 'UA-6238973-1', 'auto');
//ga('send', 'pageview');

//Function to open a new window
//==================================================
function OpenNewWindow(url, name, features) {
    window.open(url, name, features);
}

//Enforces validation on checkboxes
//==================================================
// extend range validator method to treat checkboxes differently
var defaultRangeValidator = $.validator.methods.range;
$.validator.methods.range = function (value, element, param) {
    if (element.type === 'checkbox') {
        // if it's a checkbox return true if it is checked
        return element.checked;
    } else {
        // otherwise run the default validation function
        return defaultRangeValidator.call(this, value, element, param);
    }
}

//Plain JavaScript for input validation.
//Old IE, like 7 and 8, do not validate the required attribute.
//Also, any text placed as 'placeholder' will be returned as the element's value if the actual value is empty.
$('#btnSubmit').on('click', function (event) {
    var formCollection = $('#frmsubmit').serializeArray();

    //Default to true.
    var isValid = true;

    $.each(formCollection, function (i, field) {
        try {
            //Try and get the DOM element by id, which is the name in the form collection.
            var el = $('#' + field.name);

            //Guard against elements not in the DOM, cannot find by id.
            if (el != null) {
                //Only if the element has a validation attribute.
                if (el.attr('data-val') == 'true') {
                    //This means the element's value is still the default placeholder string, or empty.
                    if (el.val() == '' || el.attr('placeholder').indexOf(el.val()) == 0) {

                        //We need to check if this element is visible or not, to exclude elements inside hidden divs.
                        //Using 'two-level' parent since the element will be in a bootstrap div and product div.
                        if (el.parent().parent().is(':visible')) {
                            //Highlight the element as required.
                            el.css('border', '2px solid #a94442');
                            isValid = false;
                        }
                    }
                }
            }
        } catch (err) { }
    });

    //Prevent the form submission.
    if (!isValid) {
        event.preventDefault();
    }
});