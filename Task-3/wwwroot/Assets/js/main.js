let items = document.querySelectorAll('.car-list .item');

items.forEach(e => {
    e.onclick = function() {
        document.querySelector('.car-list .item.active').classList.remove('active');
        this.classList.add('active')

        document.querySelector('.content-box .img-box.show').classList.remove('show');
        document.querySelector('.info .info-list.show').classList.remove('show');
        let tab = document.querySelectorAll(this.getAttribute('data-target'));
        tab.forEach(e => {
            e.classList.add("show")
        })
        tab.classList.add('show')
    }
})