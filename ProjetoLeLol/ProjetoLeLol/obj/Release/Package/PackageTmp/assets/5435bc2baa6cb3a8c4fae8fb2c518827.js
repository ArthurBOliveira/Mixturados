(function (a) {
    a(function () {
        var h; h = ""; a.each(a.browser, function (n, g) { g && "version" !== n && (h = "msie" === n ? "ie" + a.browser.version.replace(".0", "") : n) }); a(document.body).addClass(h); a("#dropdown-admin").on("reposition", function () { a(this).css({ minWidth: a(this).closest("[data-dropdown]").outerWidth() + 2 }) }); a(".chosen").livequery(function () { var h = a(this), g = { disable_search_threshold: 10 }; if (!0 === h.data("deselect")) g.allow_single_deselect = !0; h.chosen(g) }); a(".url-selection").on("change", function (h) {
            h.preventDefault();
            if ((h = a(this).val()) && 0 < h.length) document.location = h
        }); a(".show").show(); a("[data-ajaxload]").livequery(function () { var h = a(this), g = h.data("ajaxload"); setTimeout(function () { a.get(g, function (a) { h.data("original", h.html()); h.html(a) }) }, 1E3) }); a('[data-select="2"]').livequery(function () {
            var h = a(this), g = a.extend({ placeholder: "Type to search" }, h.data()); g.ajax && a.extend(g, {
                ajax: { url: g.ajax, quietMillis: 300, dataType: "json", data: function (a) { return { search: a } }, results: function (a) { return { results: a.data } } }, formatResult: function (a) {
                    if ("string" ===
                    typeof a) return a; if (a.html) return a.html; if (a.id && a.text) { var d = "<div>"; a.image && (d += '<img src="' + a.image + '" alt="' + a.title + '">'); d += a.text; return d + "</div>" } return ""
                }
            }); g.create && a.extend(g, { createSearchChoice: function (e, d) { return 0 === a(d).filter(function () { return 0 === this.text.localeCompare(e) }).length ? { id: e, text: e } : null } }); if (h.is("input") && 0 < h.val().length) g.initSelection = function (e, d) {
                var b = null, h = [], l = []; try { b = a.parseJSON(e.val()) } catch (n) { b = e.val() } b && ("object" === typeof b ? a.each(b, function (a,
                b) { l.push({ id: a, text: b }); h.push(a) }) : "string" === typeof b && a.each(b.split(","), function (a, b) { l.push({ id: b, text: b }); h.push(b) }), g.multiple || (l = l[0]), h = h.join(","), e.val(h), d(l))
            }; h.select2(g)
        }); a(".dot").livequery(function () { a(this).dotdotdot() }); setTimeout(function () { a("div.sponsors").carouFredSel({ width: 555, height: 45, align: "center", items: { visible: "variable" }, scroll: { items: 1, duration: 1250 } }) }, 1E3); a(".mobile-menu > a ").on("click", function (h) { h.preventDefault(); a(".show-mobile-menu").toggleClass("active") });
        a(".search-form").on("keypress", ".term", function (h) { 13 === h.which && a(this).closest("form").trigger("submit") })
    })
})(window.jQuery);