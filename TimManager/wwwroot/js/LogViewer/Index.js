let passColorOnTableRowByLevels = () => {
    const elementNodeListOfLevelEvents = document.querySelectorAll('tr[data-level]');

    elementNodeListOfLevelEvents.forEach(value => {
        let dataLevelAttribute = value.getAttribute('data-level');
        let colorClass = null;
        
        switch (dataLevelAttribute.toLowerCase()) {
            case 'trace':
                colorClass = 'table-secondary'
                break;
            case 'debug':
                colorClass = 'table-primary'
                break;
            case 'info':
                colorClass = 'table-info'
                break;
            case 'warn':
                colorClass = 'table-warning'
                break;
            case 'error':
                colorClass = 'table-danger'
                break;
            case 'fatal':
                colorClass = 'bg-danger'
                break;
        }

        if (colorClass !== null) {
            value.classList.add(colorClass)            
        }
    })
}

window.addEventListener('load', (event) => {
    passColorOnTableRowByLevels();
    console.log('page is fully loaded');
});

// let elementByIdBtnGetLogItemsByFile = document.getElementById('BtnGetLogItemsByFile');
// const fileField = document.querySelector('input[type="file"]');
//
// elementByIdBtnGetLogItemsByFile.addEventListener('click', ev => {
//     let files = fileField.files;
//    
//     if (files.length !== 1){
//         return;
//     }
//    
//     let formData = new FormData();
//     let file = files[0];
//     formData.append("fileInput", file);
//
//     fetch('LogViewer/GetLogItems', {
//         method: 'POST',
//         data: formData
//         // body: JSON.stringify({}),
//     })
//         .then(data => {
//             return data.json();
//         })
//         .then(jsonLogItem => {
//             alert(jsonLogItem);
//         })
//         .catch(error => {
//             console.log('Error:', error);
//         });
//    
// })