function printarFotoEscolhida() {

    const $ = document.querySelector.bind(document);
    const previewImg1 = $('.preview-img1');
    const fileChooser1 = $('.file-chooser1');
    const previewImg2 = $('.preview-img2');
    const fileChooser2 = $('.file-chooser2');
    const previewImg3 = $('.preview-img3');
    const fileChooser3 = $('.file-chooser3');

    fileChooser1.onchange = e => {
        const fileToUpload = e.target.files.item(0);
        const reader = new FileReader(); 
        reader.onload = e => previewImg1.src = e.target.result;
        reader.readAsDataURL(fileToUpload);
    };

    fileChooser2.onchange = e => {
        const fileToUpload = e.target.files.item(0);
        const reader = new FileReader();
        reader.onload = e => previewImg2.src = e.target.result;
        reader.readAsDataURL(fileToUpload);
    };

    fileChooser3.onchange = e => {
        const fileToUpload = e.target.files.item(0);
        const reader = new FileReader();
        reader.onload = e => previewImg3.src = e.target.result;
        reader.readAsDataURL(fileToUpload);
    };
}
printarFotoEscolhida();