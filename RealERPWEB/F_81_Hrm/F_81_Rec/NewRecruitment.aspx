<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="NewRecruitment.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_81_Rec.NewRecruitment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .mt20 {
            margin-top: 20px;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .card-body {
            min-height: 534px !important;
        }

        .pd4 {
            padding: 4px !important;
        }

        table {
            border: 1px solid #CCC;
            border-collapse: collapse;
        }

        td, th {
            border: none;
        }

        .grvHeader {
            background: none !important;
        }

        .card-header {
            padding: 0.2rem 1rem !important;
        }
    </style>
   
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });
        }

        function ViewEmpModal() {
            $('#ViewEmpModal').modal('toggle');
        }
        

    </script>





    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <%--      <div class="card mt-3">
   
                <div class="card-body">--%>
            <div class="row mt-5">
                <div class="col-4">

                    <div class="card">
                        <div class="card-header">
                            <span class="card-title text-muted"><strong>Add New </strong></span>

                        </div>
                        <div class="card-body">



                            <asp:GridView ID="gvNewRec" runat="server" AutoGenerateColumns="False" BorderStyle="None" Width="100%"
                                 howFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgcode" ClientIDMode="Static" runat="server" Visible="false" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgdesc" runat="server" Width="130px" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:TextBox ID="txtgvVal" ClientIDMode="Static" runat="server" BackColor="Transparent" CssClass="ml-1 form-control form-control-sm"
                                                AutoPostBack="true" ></asp:TextBox>
                                 
                                            <asp:TextBox ID="txtarea" ClientIDMode="Static" runat="server" BackColor="Transparent" CssClass="ml-1 form-control " TextMode="MultiLine"
                                                AutoPostBack="true"></asp:TextBox>


                                            <div class="form-group">
                                                <asp:DropDownList ID="ddldesig" runat="server" CssClass="custom-select chzn-select ">
                                                </asp:DropDownList>
                                            </div>

                       <div class="form-group">
                               
                                <asp:TextBox ID="txtjoindat" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtjoindat_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtjoindat"></cc1:CalendarExtender>
                            </div>


                                            <div class="form-group">

                                                <asp:FileUpload ID="imgFileUpload" CssClass="form-control form-control-sm" runat="server" accept=".pdf" />
                                                <%--                                      <asp:RequiredFieldValidator ForeColor="Red" runat="server" ControlToValidate="imgFileUpload" ValidationGroup="group1" ErrorMessage="Please enter an image" />--%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                            <asp:LinkButton ID="lnkReset" runat="server" CssClass="btn btn-sm btn-success" Width="100px" OnClick="lnkReset_Click"><i class="fa fa-spinner"></i> Reset</asp:LinkButton>

                            <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-sm btn-primary float-right" OnClick="lnkSave_Click" Width="100px"
                                OnClientClick="return confirm('Are You Sure?')"><span class="fa fa-save " style="color:white;" aria-hidden="true"  ></span>&nbsp; Save</asp:LinkButton>
                        </div>
                    </div>

                </div>
                <div class="col-8">
                    <div class="card">
                        <div class="card-header d-flex">
                      
                            <div class="mr-auto p-2">
                                      <span class="card-title text-muted mr-auto"><strong>All list </strong></span>
                            </div>
                            <div class="ml-auto p-2">
                            <asp:HyperLink ID="lnkLetIntrfc" runat="server" NavigateUrl="~/F_81_Hrm/F_92_Mgt/LetterInterface.aspx" CssClass="btn btn-info btn-sm" Target="_blank">Letter Interface</asp:HyperLink>

                            </div>
                        </div>
                        <div class="card-body">
                            <div class="table table-sm table-responsive">
                                <asp:Label runat="server" ID="lbladvnoo" Visible="false"></asp:Label>
                                <asp:GridView CssClass=" table-striped table-hover table-bordered" ID="gvAllRec" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvAllRec_RowDataBound" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Name" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladvno" runat="server" Text='<%#Eval("advno").ToString()%>' Width="10px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblname" runat="server" Text='<%#Eval("name").ToString()%>' Width="150px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldesig" runat="server" Text='<%#Eval("desig").ToString()%>' Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Mobile">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmobile" runat="server" Text='<%#Eval("mobile").ToString()%>' Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Email">
                                            <ItemTemplate>
                                                <asp:Label ID="lblemail" runat="server" Text='<%#Eval("email").ToString()%>' Width="150px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Present Address" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpereadd" runat="server" Text='<%#Eval("peradd").ToString()%>' Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Present Address" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpreadd" runat="server" Text='<%#Eval("preadd").ToString()%>' Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Department" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldept" runat="server" Text='<%#Eval("dept").ToString()%>' Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Operation" Visible="false">
                                            <ItemTemplate>
                                                <div class="dropdown">
                                                    <button type="button" class="btn btn-default btn-xs dropdown-toggle" data-toggle="dropdown">
                                                        Action
                                                        
                                                    </button>
                                                    <ul class="dropdown-menu">

                                                        <li>
                                                            <asp:HyperLink ID="lnkOfferLetter" Target="_blank"
                                                                NavigateUrl='<%# "~/LetterDefault?Type=10003 &Page=NewRec &Entry=offer Letter &advno="+Eval("advno") %>'
                                                                CssClass="dropdown-item " runat="server">Offer Letter</asp:HyperLink>
                                                        </li>

                                                        <li>
                                                            <asp:HyperLink ID="lnkAppoint" Target="_blank"
                                                                NavigateUrl='<%# "~/LetterDefault?Type=10002 &Page=NewRec &Entry=appoinment Letter &advno="+Eval("advno") %>'
                                                                CssClass="dropdown-item" runat="server">Appoinment Letter</asp:HyperLink>
                                                        </li>

                                                   

                                                        
                                                        <li>
                                                                      
                                                            
                                                            
                                                            <asp:HyperLink ID="lnkConfirmation" Target="_blank"
                                                                           NavigateUrl='<%# "~/LetterDefault?Type=10025 &Page=NewRec &Entry=confirmation Letter &advno="+Eval("advno") %>'
                                                                           CssClass="dropdown-item" runat="server">Confirmation Letter</asp:HyperLink>

                                                    
                                                            </li>


                                                                       <li>
                                                            <asp:HyperLink ID="HyperLink1" Target="_blank"
                                                                           NavigateUrl='<%# "~/LetterDefault?Type=10025 &Page=NewRec &Entry=confirmation Letter &advno="+Eval("advno") %>'
                                                                           CssClass="dropdown-item" runat="server">Reject Offer Letter</asp:HyperLink>
                                                            </li>

                                                                                <li>
                                                            <asp:HyperLink ID="HyperLink2" Target="_blank"
                                                                           NavigateUrl='<%# "~/F_81_Hrm/F_92_Mgt/InterfaceHR" %>'
                                                                           CssClass="dropdown-item" runat="server">All Letter</asp:HyperLink>
                                                            </li>




                                                    </ul>
                                                </div>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Action">

                                            <ItemTemplate>






                                                                    <asp:HyperLink ID="lblJoinnig" runat="server" Target="_blank" Width="80px"
                                                                           NavigateUrl='<%# "~/F_81_Hrm/F_82_App/EmpEntry02?Type=10025 &Page=NewRec &advno="+Eval("advno") %>'
                                                                        CssClass='<%#(Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")).Trim()=="") ? "badge badge-primary active ": "badge badge-info  disabled " %>'>    
                              <span><%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")).Trim()=="")?"Apply Joinning":"Joined "%></span>

                                                                    </asp:HyperLink>

                                                <asp:LinkButton ID="lnkView" runat="server" CssClass="text-primary pr-2 pl-2" OnClick="lnkView_Click"><i class="fa fa-eye"></i></asp:LinkButton>

                                                <asp:LinkButton ID="btnRemove" runat="server" OnClientClick="return confirm('Are You Sure?')" OnClick="btnRemove_Click" CssClass="text-danger pr-2"><i class="fa fa-trash"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="text-primary" OnClick="btnEdit_Click"><i class="fa fa-edit"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>





                                        <%--                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btn_edit" runat="server" CssClass="btn-sm text-info" OnClick="btn_edit_Click"> <i class="fa fa-edit"></i> 
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btn_remove" runat="server" CssClass="btn-sm text-danger" OnClick="btn_remove_Click"> <i class="fa fa-trash"></i> 
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="modal fade" id="ViewEmpModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel"></h5>
                            <asp:LinkButton ID="ModalClose" runat="server" CssClass="close close_btn" OnClientClick="CloseModal();" data-dismiss="modal"> &times; </asp:LinkButton>
                        </div>
                        <div class="modal-body">
                            <div class="card">
                                <div class="card-header">Details </div>
                                <div class="card-body" style="min-height: 400px;">
                                    <p><strong>Name :</strong> <span id="name" runat="server"></span></p>
                                    <p><strong>Designation :</strong> <span id="desig" runat="server"></span></p>
                                    <p><strong>Department :</strong> <span id="dept" runat="server"></span></p>
                                    <p><strong>Email :</strong> <span id="email" runat="server"></span></p>
                                    <p><strong>Mobile :</strong> <span id="mobile" runat="server"></span></p>

                                    <p><strong>Present Address :</strong> <span id="preadd" runat="server"></span></p>
                                    <p><strong>Permanent Address :</strong> <span id="peradd" runat="server"></span></p>








                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

