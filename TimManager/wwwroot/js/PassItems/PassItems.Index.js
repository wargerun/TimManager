const modalPassItemEditElm = document.getElementById("modalPassItemEdit");
const modalPassItemDeleteElm = document.getElementById("modalPassItemDelete");

const modalTitleNameElm = document.getElementById("modalTitleName");
const modalPassItemNameElm = document.getElementById("modalPassItemName");
const modalPassItemUserNameElm = document.getElementById("modalPassItemUserName");
const modalPassItemPasswordElm = document.getElementById("modalPassItemPassword");
const modalPassItemUriElm = document.getElementById("modalPassItemUri");
const modalPassItemCreatedElm = document.getElementById("modalPassItemCreated");
const modalPassItemModifiedElm = document.getElementById("modalPassItemModified");
const modalPassItemDescriptionElm = document.getElementById("modalPassItemDescription");

let btnShowModalDetailsElements = document.querySelectorAll(".btn-show-modal-details");

let handleGetPassItemMethod = (passItem) => {
    modalTitleNameElm.innerHTML = null;
    modalPassItemNameElm.innerHTML = null;
    modalPassItemUserNameElm.innerHTML = null;
    modalPassItemPasswordElm.removeAttribute('data-itemId');
    modalPassItemUriElm.innerHTML = null;
    modalPassItemCreatedElm.innerHTML = null;
    modalPassItemModifiedElm.innerHTML = null;
    modalPassItemDescriptionElm.innerHTML = null;

    if (passItem) {
        modalTitleNameElm.innerText = passItem.name;
        modalPassItemNameElm.innerText = passItem.name;
        modalPassItemUserNameElm.innerText = passItem.userName;

        modalPassItemPasswordElm.setAttribute('data-itemId', passItem.id);

        modalPassItemUriElm.href = passItem.uri;
        modalPassItemUriElm.innerText = passItem.uri;
        modalPassItemCreatedElm.innerText = passItem.created;
        modalPassItemModifiedElm.innerText = passItem.modified;

        if (passItem.description) {
            passItem.description.split(/\n/).forEach(item => {
                let li = document.createElement("li");
                li.className = 'btn-copy list-group-item';
                li.textContent = item;
                li.onclick = function () {
                    showNotifyCopied(li);
                    copyToClipboard(item);
                    return false;
                }

                modalPassItemDescriptionElm.appendChild(li);
            });
        }

        modalPassItemEditElm.href = document.location + '/Edit/' + passItem.id;
        modalPassItemDeleteElm.href = document.location + '/Delete/' + passItem.id;
    }
}

let ajaxGetPassItem = (itemId, handlePassItem) => {
    fetch('PassItems/GetPassItem?id=' + itemId, {
        method: 'GET',
        //credentials: 'same-origin',
        headers: {
            'X-Requested-With': 'XMLHttpRequest',
            'contentType': "application/json; charset=utf-8",
        }
    })
        .then(data => {
            return data.json();
        })
        .then(jsonPassItem => {
            handlePassItem(jsonPassItem);
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

btnShowModalDetailsElements.forEach(item => {
    item.addEventListener("click", (itemListener) => {
        let itemId = Number(itemListener.target.getAttribute("data-itemId"));
        console.log('clicked on itemid: ' + itemId + '  ' + typeof itemId);
        modalTitleNameElm.innerText = 'Loading..';

        ajaxGetPassItem(itemId, handleGetPassItemMethod);
    });
});

let passwordCopyToClipboardById = document.querySelectorAll(".PasswordCopyToClipboardById");

passwordCopyToClipboardById.forEach(item => {
    item.addEventListener("click", () => {
        let itemId = item.getAttribute("data-itemId");

        ajaxGetPassItem(itemId, passItem => {
            let toClickboard = copyToClipboard(passItem.password);
            console.log(toClickboard === true ? 'Copied successfully' : 'Copied failed');
        });
    });
});