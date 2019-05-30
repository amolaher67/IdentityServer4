# IdentityServer4
Sample API Application for Authentication

Few Key points to remember
1. Certificate should be place on Certificat stor when we go to Production
2. For dev we can use it from local machine

Steps to create Self Sigh Certificate using openssl software from command promt.
subject="/CN=LedgerChargesAPI"

generate Cerificate and key

Step 1
openssl req -x509 -newkey rsa:4096 -sha256 -nodes -keyout LedgerChargesAPI.key -out LedgerChargesAPI.crt -subj "/CN=LedgerChargesAPI" -days 3650

Step 2
openssl pkcs12 -export -out LedgerChargesAPI.pfx -inkey LedgerChargesAPI.key -in LedgerChargesAPI.crt -certfile LedgerChargesAPI.crt
