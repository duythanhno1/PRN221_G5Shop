// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#addProductButton').on('click', function () {
        $('#addProductModal').modal('show');
    });
    $('#closeProduct').on('click', function () {
        $('#addProductModal').modal('hide');
    });
    $('#saveProduct').on('click', function () {
        var productName = $('#productName').val();
        var productPrice = $('#productPrice').val();
        var productImgPath = $('#productImgPath').val();
        var isAvailable = $('#isAvailable').val();
        var productCategory = $('#productCategory').val();
        // Validate input data here
        // Send data to the server (possibly using AJAX)
        $('#addProductModal').modal('hide');
    });
    // Xử lý khi nút "Edit" được nhấn
    $('#editProductButton').on('click', function () {
        $('#editProductModal').modal('show');
    });
    $('#closeEdit').on('click', function () {
        $('#editProductModal').modal('hide');
    });
    // Xử lý khi nút "Save" trong modal "Edit Product" được nhấn
    $('#saveEdit').on('click', function () {
        var editedProductName = $('#editedProductName').val();
        var editedProductPrice = $('#editedProductPrice').val();
        var editedProductImgPath = $('#editedProductImgPath').val();
        var editedIsAvailable = $('#editedIsAvailable').val();
        // Thực hiện lưu thông tin sản phẩm đã chỉnh sửa tại đây
        $('#editProductModal').modal('hide');
    });
});


