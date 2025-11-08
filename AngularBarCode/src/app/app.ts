import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, HttpClientModule, FormsModule],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App implements OnInit {
  barcodes: any[] = [];
  newBarcode: string = '';
  //apiUrl = 'https://localhost:44337/api/barcode';
  apiUrl = 'http://localhost:5222/api/barcode';

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.loadBarcodes();
  }

  loadBarcodes() {
    this.http.get<any[]>(this.apiUrl).subscribe({
      next: (res) => (this.barcodes = res),
      error: (err) => console.error('โหลดข้อมูลล้มเหลว:', err)
    });
  }

  addBarcode() {
    if (!this.newBarcode) {
      Swal.fire('กรุณากรอกรหัสสินค้า', '', 'warning');
      return;
    }

    const pattern = /^[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}$/;
    if (!pattern.test(this.newBarcode)) {
      Swal.fire('รูปแบบรหัสสินค้าไม่ถูกต้อง', 'กรุณาระบุ ตัวเลขหรือตัวอักษรภาษาอังกฤษ ให้ครบ 16 หลักเท่านั้น', 'error');
      return;
    }
    //call api
    const newItem = { code: this.newBarcode };
    this.http.post(this.apiUrl, newItem).subscribe({
      next: () => {
        Swal.fire({
          title: 'เพิ่มสำเร็จ!',
          text: 'รหัสสินค้าถูกเพิ่มเรียบร้อยแล้ว',
          icon: 'success',
          timer: 1200,
          showConfirmButton: false
        });
        this.newBarcode = '';
        this.loadBarcodes();
      },
      error: (err) => {
        console.error(err);
        Swal.fire('เกิดข้อผิดพลาด', 'ไม่สามารถเพิ่มข้อมูลได้', 'error');
      }
    });
  }
  //delete
  deleteBarcode(item: any) {
    Swal.fire({
      title: 'คุณแน่ใจหรือไม่?',
      text: `ต้องการลบรหัสสินค้า ${item.code} ใช่หรือไม่?`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'ยืนยัน',
      cancelButtonText: 'ยกเลิก',
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33'
    }).then((result) => {
      if (result.isConfirmed) {
        this.http.delete(`${this.apiUrl}/${item.id}`).subscribe({
          next: () => {
            Swal.fire({
              title: 'ลบข้อมูลเรียบร้อยแล้ว',
              text: `รหัสสินค้า ${item.code} ถูกลบเรียบร้อยแล้ว`,
              icon: 'success',
              timer: 1200,
              showConfirmButton: false
            });
            this.loadBarcodes();
          },
          error: (err) => {
            console.error(err);
            Swal.fire('เกิดข้อผิดพลาด', `ไม่สามารถลบรหัสสินค้า ${item.code} ได้`, 'error');
          }
        });
      }
    });
  }

  //genBarcode
  generateBarcode(): void {
    const chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
    let raw = '';
    for (let i = 0; i < 16; i++) {
      raw += chars.charAt(Math.floor(Math.random() * chars.length));
    }
    this.newBarcode = raw.replace(/(.{4})/g, '$1-').slice(0, -1);
  }
  //format
  formatBarcode(): void {
    if (!this.newBarcode) return;
    let cleaned = this.newBarcode.toUpperCase().replace(/[^A-Z0-9]/g, '');
    cleaned = cleaned.substring(0, 16);
    this.newBarcode = cleaned.replace(/(.{4})/g, '$1-').slice(0, -1);
  }
}
