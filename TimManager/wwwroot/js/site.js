var copyToClipboard = (text) => {
    if (window.clipboardData && window.clipboardData.setData) {
        // IE specific code path to prevent textarea being shown while dialog is visible.
        return clipboardData.setData("Text", text);

    } else if (document.queryCommandSupported && document.queryCommandSupported("copy")) {
        var textarea = document.createElement("textarea");

        textarea.textContent = text;
        textarea.style.position = "fixed";  // Prevent scrolling to bottom of page in MS Edge.
        document.body.appendChild(textarea);
        textarea.select();

        try {
            return document.execCommand("copy");  // Security exception may be thrown by some browsers.
        } catch (ex) {
            console.warn("Copy to clipboard failed.", ex);
            return false;
        } finally {
            document.body.removeChild(textarea);
        }
    }
}

let btnCopyElements = document.querySelectorAll('.btn-copy');
const defaultTooltip = 'Copy to clickboard';
const styleForAnimation = 'btn-copy-after';

let showNotifyCopied = (element) => {
    // remove old all tooltip
    element.querySelectorAll('.' + styleForAnimation).forEach(item => {
        item.remove();
    });

    let divElement = document.createElement('span')
    divElement.textContent = 'Copied!';
    divElement.classList.add(styleForAnimation);

    element.appendChild(divElement);

    setTimeout(function () {
       divElement.remove();
    }, 2000);
}

btnCopyElements.forEach(element => {
    element.addEventListener('click', eventElement => {
        showNotifyCopied(element);
        let valueForCopy = element.getAttribute('data-itemValue');

        if (!valueForCopy) {
            valueForCopy = element.innerText;
        }

        const toClickboard = copyToClipboard(valueForCopy);
        console.log('Copied: ' + valueForCopy);
    });
});